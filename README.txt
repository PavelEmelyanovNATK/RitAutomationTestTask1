Задание:
На языке C# необходимо разработать приложение Windows Forms ( ОБЯЗАТЕЛЬНО .NET Framework ), которое на главной форме содержит карту из библиотеки GMap.NET, загружает из базы данных Microsoft SQL Server географические координаты условных единиц техники и отображает их на карте в виде маркеров. Так же необходимо реализовать перемещение маркеров с помощью мыши (Drag&Drop, то есть нажать на маркер и перенести в другую точку карты) и сохранение новых координат в базу данных, чтобы после перезапуска приложения маркеры были расположены так же, как и до закрытия приложения.

---------------------------------

Скрипт БД - MachinesMap.sql

В классе MapDbDao в поле ServerName нужно указать название сервера MS SQL.
MapDbDao находится в Domain -> Services -> MapDbDao.
