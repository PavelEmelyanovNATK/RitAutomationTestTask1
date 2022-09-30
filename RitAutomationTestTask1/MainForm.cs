using GMap.NET;
using GMap.NET.WindowsForms;
using RitAutomationTestTask1.Domain.Services.MapDbDao;
using RitAutomationTestTask1.Domain.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RitAutomationTestTask1
{
    public partial class MainForm : Form
    {
        private readonly GMapOverlay _markersOverlay;
        private GMapMarker _hoveredMarker;
        private bool _isMarkerDragging;
        private Point _markerOffset;
        private IAsyncMapDao _dao;

        public MainForm()
        {
            InitializeComponent();

            _markersOverlay = new GMapOverlay("MarkersOverlay");
            InitializeMap();
        }

        private void InitializeMap()
        {
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            Map.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            Map.DragButton = MouseButtons.Left;

            Map.Overlays.Add(_markersOverlay);
        }

        private void MainFormOnLoad(object sender, EventArgs e)
        {
            Map.Position = new PointLatLng(55.030204, 82.920430);

            _dao = new EntityFrameworkMapDao();
            LoadMarkersAsync();
        }

        private async void LoadMarkersAsync()
        {
            try
            {
                var markers = await _dao.GetMachineMarkersAsync();

                foreach (var marker in markers)
                {
                    _markersOverlay.Markers.Add(GMapUtils.CreateMarker(marker.Machine.Id, marker.Machine.Name, marker.Latitude, marker.Longitude));
                }

                Map.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        private async void UpdateMarkerInfoAsync(GMapMarker marker)
        {
            try
            {
                var markerId = (Guid)marker.Tag;
                await _dao.UpdateMachinePositionAsync(markerId, marker.Position.Lat, marker.Position.Lng);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
            }
        }

        #region Markes dragging logic
        private void MapOnMarkerEnter(GMapMarker item)
        {
            if (_isMarkerDragging) return;

            _hoveredMarker = item;
        }

        private void MapOnMarkerLeave(GMapMarker item)
        {
            if (_isMarkerDragging) return;

            _hoveredMarker = null;
        }

        private void MapOnMouseDown(object sender, MouseEventArgs e)
        {
            if (_hoveredMarker != null)
            {
                _isMarkerDragging = true;

                var localPos = Map.FromLatLngToLocal(_hoveredMarker.Position);
                _markerOffset = new Point(e.X - (int)localPos.X, e.Y - (int)localPos.Y);
            }
            else _isMarkerDragging = false;
        }

        private void MapOnMouseUp(object sender, MouseEventArgs e)
        {
            _isMarkerDragging = false;

            if (_hoveredMarker != null)
            {
                UpdateMarkerInfoAsync(_hoveredMarker);
            }
        }

        private void MapOnMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMarkerDragging) return;

            var mouseCoords = Map.FromLocalToLatLng(e.X - _markerOffset.X, e.Y - _markerOffset.Y);

            _hoveredMarker.Position = mouseCoords;
        }
        #endregion
    }
}
