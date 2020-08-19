# Интернет магазин (ASP.NET MVC5)

## Краткое описание работы сайта

Домашняя страница:

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D0%B4%D0%BE%D0%BC.png?raw=true)

Регистрация:

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D1%80%D0%B5%D0%B3%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%86%D0%B8%D1%8F.png?raw=true)

Подтверждение регистрации:

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D1%80%D0%B5%D0%B3%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%86%D0%B8%D1%8F%20%D0%BF%D1%80%D0%BE%D0%B4%D0%BE%D0%BB%D0%B6%D0%B5%D0%BD%D0%B8%D0%B5.png?raw=true)

На указанную почту придёт ссылка для подтверждения регистрации:

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D1%81%D1%81%D1%8B%D0%BB%D0%BA%D0%B0%20%D1%80%D0%B5%D0%B3%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%86%D0%B8%D0%B8.png?raw=true)


Вход на сайт:

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D0%B2%D1%85%D0%BE%D0%B4%20%D0%BD%D0%B0%20%D1%81%D0%B0%D0%B9%D1%82.png?raw=true)

в правом углу появится имя пользователя:

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D0%B0%D0%B2%D1%82%D0%BE%D1%80%D0%B8%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%BD%D1%8B%D0%B9%20%D0%B2%D1%85%D0%BE%D0%B4%20%D0%BD%D0%B0%20%D1%81%D0%B0%D0%B9%D1%82.png?raw=true)

Покупка:
 Товар автоматически попадает в корзину,где учитывается количество позиций и общая сумма, которая отображается в правом углу,
 при продолжении "листания" сайта

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D0%BA%D0%BE%D1%80%D0%B7%D0%B8%D0%BD%D0%B0%20%D0%BF%D0%BE%D0%BA%D1%83%D0%BF%D0%BE%D0%BA.png?raw=true)

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D0%BE%D0%B1%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%BA%D0%B0%20%D0%B7%D0%B0%D0%BA%D0%B0%D0%B7%D0%B0.png?raw=true)

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D1%81%D0%BF%D0%B0%D1%81%D0%B8%D0%B1%D0%BE%20%D0%B7%D0%B0%20%D0%BF%D0%BE%D0%BA%D1%83%D0%BF%D0%BA%D1%83.png?raw=true)

после завершения оформления покупки на указанную почту придёт сообщение об отправке товара

![Alt-текст](https://github.com/E-A-Volobuev/BootSale/blob/master/%D1%81%D0%BE%D0%BE%D0%B1%D1%89%D0%B5%D0%BD%D0%B8%D0%B5%20%D0%BE%D0%B1%20%D0%BE%D1%82%D0%BF%D1%80%D0%B0%D0%B2%D0%BA%D0%B5.png?raw=true)

##  Модели

Проект создан с типом аутентификации 'No Authentication'. Вся логика авторизации и аутентификации добавлена вручную.
Класс пользователя 'ApplicationUser' наследуется от базового класса IdentityUser, при авторизации и аутентификации
вся работа идёт не напрямую с 'ApplicationUser', а через 'ApplicationUserManager',который наследует весь функционал 
от базового класса  'UserManager'.

Для устранения жёсткой связи между контроллерами и моделями в проект внедрены зависимости (библиотека Ninject для 
создания специального распознавателя зависимостей, который MVC Framework будет применять при создании экземпляров 
объектов внутри приложения). 

Созданы интерфейсы ICartRepository,ICategoryRepository,IProductRepository, методы которых и используют контроллеры.

Класс 'Category' описывает категорию товара.
Класс 'Product' описывает сущность товара.
Класс 'LoginModel' модель для аутентификации.
Класс 'RegisterModel' модель для авторизации.
Класс 'Cart' описывает корзину покупок и базовые действия, совершаемые в ней, наследует методы ICartRepository.
Класс 'Purchase' модель для покупки товара.
Класс 'PagInfo' модель для создания пагинации.
Класс репозитория 'ProductRepository' содержит логику действий с продуктом, наследует методы IProductRepository.
Класс репозитория 'CategoryRepository' содержит логику действий с продуктом, наследует методы ICategoryRepository.

Для обращения к базе данных используется технология Entity Framework.

Класс контекста данных 'ProductContext' наследует свойства базового класса 'IdentityDbContext'.
Класс 'ProductDbInitializer'служит для инициализации базы данных начальными значениями.
Класс 'CartIndexViewModel' служит для инициализации корзины покупок.


##  Контроллеры

В каждом контроллере применены внедрения зависимостей (контроллеры используют методы интерфейсов)
Логика авторизации и аутентификации представлена в контроллере 'AccountController'.
В методе 'Register'  представлен механизм подтверждения регистрации.
С помощью 'SmtpClient' пользователю направляется письмо,содержашее ссылку для подтверждения регистрации.

Логика разбития контента на страницы (пагинация) представлена в 'HomeController'.
Так же в 'HomeController' реализован способ загрузки на сервер изображения товаров.
В базе данных хранятся ссылки, а сами изображения на сервере.

Логика действий с  продуктом представлена в 'ProductsController'.
Логика навигации по сайту представлена в 'NavController'.
Логика добавления товара в корзину , совершения покупки и email-оповещения о покупке представлена в 
'CartController'.
