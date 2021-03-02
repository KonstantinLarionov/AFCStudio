$(document).ready(function () {
    $('#logo').animate({ opacity: 1 }, 1000);
    $('#area').animate({ opacity: 1 }, 2000);
    $('#my_question').text('Какой продукт вы хотите?');
    for (var key in Struct) {
        $('<button onclick="Product(' + "'" + key + "'" + ')">' + Struct[key].Title + '</button>').addClass("text-white btn btn-outline-danger w-75 p-2")
            .appendTo(($('<div></div>').addClass("row m-3 align-items-center justify-content-center"))
                .appendTo($('#my_answers')));
    }
});
function Product(f) {
    var c = Struct[f].Categories;
    product = f;
    request.product = Struct[f].Title;
    $('#my_question').text(Struct[f].Question);
    $('#my_answers').empty();
    for (var key in c) {
        $('<button onclick="Category(' + "'" + key + "'" + ')">' + c[key].Title + '</button>').addClass("text-white btn btn-outline-danger w-75 p-2")
            .appendTo(($('<div></div>').addClass("row m-3 align-items-center justify-content-center"))
                .appendTo($('#my_answers')));
    }
}//&nbsp <small>(' + c[key].Cost +' ₽)</small>
function Category(f) {
    category = f;
    request.category = Struct[product].Categories[category].Title;
    amount += +Struct[product].Categories[category].Cost;
    Step(-1);
}
function Step(f) {
    var q = Struct[product].Categories[category].Questions;
    if (step == q.length) {
        $('#my_question').text('У вас интересный заказ! Мы могли бы с вами поработать..');
        $('#my_amount').text(amount + ' ₽');
        $('#my_answers').css('display', 'none');
        $('#contacts').css('display', '');
        return;
    }

    $('#my_question').text(q[step].Text);
    $('#my_answers').empty();
    if (f !== -1) {
        amount += +q[step].Answers[f].Cost;
        //swal('question ' + q[step - 1].Text + '/n answer' + q[step - 1].Answers[f].Text);
        request.questions.push({ 'question': q[step - 1].Text, 'answer': q[step - 1].Answers[f].Text, 'cost': q[step - 1].Answers[f].Cost });
    }
    var i = 0;
    for (var key in q[step].Answers) {
        $('<button onclick="Step(' + i++ + ')">' + q[step].Answers[key].Text + '</button>').addClass("text-white btn btn-outline-danger w-75 p-2")
            .appendTo(($('<div></div>').addClass("row m-3 align-items-center justify-content-center"))
                .appendTo($('#my_answers')));
    }
    step++;
} //'&nbsp <small>(' + q[step].Answers[key].Cost +' ₽)</small>' +
var request = {
    category: '',
    product: '',
    amount: '',
    questions: []
};
var category = '';
var product = '';
var step = 0;
var amount = 0;
var Struct = {
    Site: {
        Title: "Сайт",
        Question: "Какой сайт вы хотите?",
        Categories: {
            Lending: {
                Title: "Лендинг",
                Cost: "3000",
                Questions: [
                    {
                        Text: "Есть ли у вас лого и название сайта?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "1000"
                            }
                        ]
                    },
                    {
                        Text: "Есть ли у вас дизайн сайта / макет?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "2000"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам чат на сайте?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1500"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ],
                    },
                    {
                        Text: "Для какой цели нужен лендинг?",
                        Answers: [
                            {
                                Text: "Реклама продажи товара",
                                Cost: "0"
                            },
                            {
                                Text: "Предоставление услуг",
                                Cost: "0"
                            },
                            {
                                Text: "Личный сайт",
                                Cost: "0"
                            },
                            {
                                Text: "Галерея",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Будет ли изменяться сайт в дальнейшем?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                ]
            },
            Multipage_business_card: {
                Title: "Многостраничная визитка",
                Cost: "5000",
                Questions: [
                    {
                        Text: "Есть ли у вас лого и название сайта?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "1000"
                            }
                        ]
                    },
                    {
                        Text: "Есть ли у вас дизайн сайта / макет (концепция)?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "5000"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам чат на сайте?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Цель создания сайта?",
                        Answers: [
                            {
                                Text: "Информация о себе",
                                Cost: "0"
                            },
                            {
                                Text: "НЕИнформация компании",
                                Cost: "0"
                            },
                            {
                                Text: "Информация о товаре",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Планируете ли вы галерею на сайте?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1500"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Планируете ли вы изменять сайт в дальнейшем?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                ]
            },
            Catalog: {
                Title: "Каталог",
                Cost: "8000",
                Questions: [
                    {
                        Text: "Есть ли у вас лого и название сайта?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "1000"
                            }
                        ]
                    },
                    {
                        Text: "Есть ли у вас дизайн сайта / макет (концепция)?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "5000"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам чат на сайте?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1500"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Планируете ли вы изменять сайт в дальнейшем?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Планируете ли вы изменить сайт каталог на сайт магазин в дальнейшем?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    }
                ]
            },
            Online_store: {
                Title: "Интернет магазин",
                Cost: "15000",
                Questions: [
                    {
                        Text: "Есть ли у вас лого и название сайта?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "1000"
                            }
                        ]
                    },
                    {
                        Text: "Есть ли у вас дизайн сайта / макет (концепция)?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "5000"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам чат на сайте?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1500"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    }
                ]
            },
            Web_application: {
                Title: "Веб-приложение",
                Cost: "20000",
                Questions: [
                    {
                        Text: "Есть ли у вас лого и название сайта?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "1000"
                            }
                        ]
                    },
                    {
                        Text: "Есть ли у вас дизайн сайта / макет (концепция)?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "5000"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам чат на сайте?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1500"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Приложение будет статичным или динамичным?",
                        Answers: [
                            {
                                Text: "Статичным с перезагрузкой страниц",
                                Cost: "0"
                            },
                            {
                                Text: "Динамичным без перезагрузки страниц",
                                Cost: "3000"
                            }
                        ]
                    },
                    {
                        Text: "Планируется ли использовать web socket?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Какого типа вы планируете приложение?",
                        Answers: [
                            {
                                Text: "Распределенное",
                                Cost: "5000"
                            },
                            {
                                Text: "Не распределенное - все на одном ресурсе",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Какую нагрузку вы планируете ?",
                        Answers: [
                            {
                                Text: "Низкая",
                                Cost: "0"
                            },
                            {
                                Text: "Средняя",
                                Cost: "2000"
                            },
                            {
                                Text: "высокая",
                                Cost: "5000"
                            }
                        ]
                    },
                ]
            }
        }
    },
    Program: {
        Title: "Программа",
        Question: "Какую программу вы хотите?",
        Categories: {
            Trade: {
                Title: "Торговля",
                Cost: "10000",
                Questions: [
                    {
                        Text: "Нужен(Важен) ли вам дизайн или макет программы?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Важна ли вам анимация переходов в программе?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Сколько типов пользователей будет в программе?",
                        Answers: [
                            {
                                Text: "Администратор, оператор, клиент",
                                Cost: "3000"
                            },
                            {
                                Text: "Администратор, оператор",
                                Cost: "2000"
                            },
                            {
                                Text: "Администратор",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужно ли вам составление отчетов ?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Будет ли осуществляться связь с сайтом если он есть?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    }
                ]
            },
            Management_clients: {
                Title: "Управление клиентами/партнерами",
                Cost: "8000",
                Questions: [
                    {
                        Text: "Нужен(Важен) ли вам дизайн или макет программы?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Важна ли вам анимация переходов в программе?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Сколько типов пользователей будет в программе?",
                        Answers: [
                            {
                                Text: "Администратор, оператор, клиент",
                                Cost: "3000"
                            },
                            {
                                Text: "Администратор, оператор",
                                Cost: "2000"
                            },
                            {
                                Text: "Администратор",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужно ли вам составление отчетов ?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Будет ли осуществляться связь с сайтом если он есть?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                ]
            },
            Chat_or_chatbots: {
                Title: "Чат или Чат-бот",
                Cost: "15000",
                Questions: [
                    {
                        Text: "Нужен(Важен) ли вам дизайн или макет программы?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Важна ли вам анимация переходов в программе?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Планируется ли использование ИИ?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "10000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                ]
            },
            Controll_and_collection_informations: {
                Title: "Контроль и сбор информации/статистики",
                Cost: "20000",
                Questions: []
            },
            Todo_list: {
                Title: "Список дел (TODO лист)",
                Cost: "13000",
                Questions: []
            },
            Robots: {
                Title: "Роботы",
                Cost: "35000",
                Questions: []
            }
        }
    },
    Promotion: {
        Title: "Продвижение",
        Question: "Тип продвижения",
        Categories: {
            Product_or_service: {
                Title: "Продукт или сервис",
                Cost: "3000",
                Questions: [
                    {
                        Text: "Нужен ли вам SEO Анализ вашего ресурса?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Продвигать каждый месяц ваш ресур в поисковиках?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Заказывать ли рекламу вашего ресурса в поисковиках на других сайтах и в группах соц сетей  в течении месяца?",
                        Answers: [
                            {
                                Text: "Да, каждую неделю",
                                Cost: "10000"
                            },
                            {
                                Text: "Да, каждые 2 недели",
                                Cost: "5000"
                            },
                            {
                                Text: "Да, каждый месяц",
                                Cost: "2500"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужно ли ва оформление групп в соц сетях?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                ]
            }
        }
    },
    Mobile_application: {
        Title: "Мобильное приложение",
        Question: "Тип приложения",
        Categories: {
            Clone_site: {
                Title: "Приложение, дублирующее сайт",
                Cost: "10000",
                Questions: [
                    {
                        Text: "Приложение дублирующее сайт?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "15000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "30000"
                            }
                        ]
                    }
                ]
            }
        }
    },
    Design: {
        Title: "Дизайн",
        Question: "Тип дизайна",
        Categories: {
            Brend: {
                Title: "Брэнд",
                Cost: "2000",
                Questions: [
                    {
                        Text: "Нужно ли вам название компании?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "0"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам логотип?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам рекламный баннер",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужны ли вам визитки?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужны ли вам пригласительные?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам макет сайта?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "5000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам акционный баннер?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам лист СЧЕТ-ФАКТУРА",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "2000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам лист СЧЕТ-ФАКТУРА",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам ценник на продукцию?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "1000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам фирменный бланк?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "3000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                    {
                        Text: "Нужен ли вам мерч дизайн?",
                        Answers: [
                            {
                                Text: "ДА",
                                Cost: "7000"
                            },
                            {
                                Text: "НЕТ",
                                Cost: "0"
                            }
                        ]
                    },
                ]
            }
        }
    }
};

function send() {
    var amount = document.getElementById("my_amount");
    var contact = document.getElementById("contact");
    let req = { text: "Новый заказ на AFCStudio Через калькулятор.\n\r Сумма: " + amount.innerHTML + "\n\r Контакт: " + contact.value }
    fetch('send_form_price.php', {
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        method: 'POST',
        body: JSON.stringify(req)
    })
        .then(response => response.text())
        .then(result => alert(result))
}
