Trans Neft Energo - Power Project
==============================

## Схема модели классов
![Схема модели](./assets/images/datascheme.png)

## Структура решения
+ `WebAPI` - Приложение ASP.Net Core 3.1, реализующее функционал REST Web API;
+ `WebAPI.Test` - Приложение ??? для тестирования;
+ `APITestConsoleApp` - Консольное приложение .Net Core для тестирования функций WebAPI;
+ `Contract` - Содержит набор общих интерфейсов и DTO, используемых на презентационном уровне для приложений `WebAPI` и `APITestConsoleApp`;
    + `Contract.Interfaces` - Содержит набор интерфейсов API для реализации в проектах;
    + `Contract.DTO` - Содержит набор DTO, возвращаемых API;
+ `Infrastructure` - Содержит контекст БД для EnergoDB, а также класс для начальной инициализации БД, и класс конфигурации контекста;
+ `Domain` - Содержит набор сущностей проекта, интерфейсы для репозитория и сервисов;
+ `Logics` - Определяет уровень бизнес-логики приложения. Содержит реализации репозиториев и сервисов.

## Зависимости

### Внешние зависимости

+ Microsoft.EntityFrameworkCore v5.0.13 - `Infrastructure`;


### Ссылки на проекты

| Проект | WebAPI | WebAPI.Test | APITestConsoleApp | Contract.Interfaces | Contract.DTO | Infrastructure | Domain | Logics |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| WebAPI | `:green_heart:` | | | | | | | |
| WebAPI.Test | | `:green_heart:` | | | | | | |
| APITestConsoleApp | | | `:green_heart:` | | | | | |
| Contract.Interfaces | | | | `:green_heart:` | | | | |
| Contract.DTO | | | | | `:green_heart:` | | | |
| Infrastructure | | | | | | `:green_heart:` | `:white_check_mark:` | |
| Domain | | | | | | `:white_check_mark:` | `:green_heart:` | |
| Logics | | | | | | | | `:green_heart:` |

## Запуск
Запускаемым проектом в решении назначен `WebAPI`. Приложение запускает веб-сервер Kestrel на порту 8050. Для быстрого теста API можно воспользоваться подключенным к проекту Swagger'ом. Чтобы запустить тестирующее API консольное приложение необходимо запустить следующий файл:

```(txt)
aaaaaaaaaaaaaaaaaaaaaaaaaa
```

## Дальнейшее развитие
+ Использование [AutoMapper'a](https://automapper.org/) для ускорения написания кода для DTO;