# AS2R Resolution Patcher
**AS2R Resolution Patcher** – это патч для Steam-версии игры [Alien Shooter 2: Reloaded](https://store.steampowered.com/app/33120/Alien_Shooter_2_Reloaded/), позволяющий устанавливать пользовательскте разрешения экрана. 

Метод работы основан на подмене разрешения в конфигурационном файле и изменении названий файлов зависимых от разрешения текстур.

## Порядок установки и использования
1. Установить [Alien Shooter 2: Reloaded](https://store.steampowered.com/app/33120/Alien_Shooter_2_Reloaded/) из [Steam](https://store.steampowered.com/) и запустить игру. Последнее необходимо для того, чтобы процессы игры создал нужный файл конфигурации. После загрузки главного меню игру необходимо закрыть.
2. Установить .Net SDK версии 7.0 или новее с официального [сайта](https://dotnet.microsoft.com/en-us/download).
2. Скачать последний [релиз](https://github.com/DUB1401/AS2R-ResolutionPatcher/releases) патча.
3. Запустить исполняемый _*.exe_ файл и убедиться, что все исходные поля имеют зелёный цвет шрифта.
4. Выбрать нужное разрешение и нажать кнопку установки патча.

# FAQ
### _Вопрос_: Поле «Steam» горит красным.

_**Ответ**_: Патч разрабатывался только для Steam-версии игры. Установите клиент [Steam](https://store.steampowered.com/) или проверьте директорию установки по умолчанию.
___
### _Вопрос_: Поле «Save folder» горит красным.

_**Ответ**_: Запустите игру, дождитесь загрузки меню и закройте её. Это необходимо для создания файла конфигурации.
___
### _Вопрос_: Одно из названий файлов красное.

_**Ответ**_: Программе не удалось найти этот файл или же он уже изменён. Выполните проверку целостности файлов игры перед установкой патча.
___
### _Вопрос_: Как мне вернуть файлы игры к исходному состоянию?

_**Ответ**_: Выполните проверку целостности файлов игры в клиенте [Steam](https://store.steampowered.com/) или скопируйте с заменой файлы из [этой](Backup/) директории в _%GAME_FOLDER%/Maps/_.
___
### _Вопрос_: Патч установлен, но меню игры всё равно выглядит маленьким.

_**Ответ**_: Патч работает только во время игрового процесса и не влияет на меню в виду программных особенностей.

# Скриншот
![2023-04-25_21-32-43](https://user-images.githubusercontent.com/40277356/234376923-c7aefefb-14d4-4a58-8483-277d602dcfce.jpg)

# Благодарность
* [@KDD!^putler kaputt 26/93](https://steamcommunity.com/id/agof) – алгоритм патча.

_Copyright © DUB1401. 2023._
