`Чтобы бот завелся нужен файл с настройками. Напишите @caimanchik в телеграме`

## Описание

Телеграм-бот, позволяющий обучающимся попросить помощи у менторов. Ученику достаточно открыть бота, выложить задание и выбрать подходящий ответ. 

У каждого ученика и ментора есть очки, можно посмотреть таблицу лидеров, информацию о себе (очки, количество выложенных задач, свой юзернэйм). 

При отправке ответа, автору задачи приходит уведомление о том, что появился новый ответ. При пометке ответа как правильный, автору ответа приходит уведомление и начисляются очки

## Примененные паттерны

### DDD

Решение разбито на три области - [приложение](https://github.com/likip3/TelegaSharpProject/tree/master/TelegaSharpProject), [доменная](https://github.com/likip3/TelegaSharpProject/tree/master/TelegaSharpProject.Domain), [инфраструктурная](https://github.com/likip3/TelegaSharpProject/tree/master/TelegaSharpProject.Infrastructure)

### OCP

Есть точки расширения. Например, можно легко добавить [кнопку](https://github.com/likip3/TelegaSharpProject/blob/master/TelegaSharpProject/Bot/Buttons/Abstracts/Button.cs) или [команду](https://github.com/likip3/TelegaSharpProject/blob/master/TelegaSharpProject/Bot/Commands/Abstracts/Command.cs), всего лишь унаследовавшись от их базовых классов

### DI-контейнер

Использована библиотека `Ninject` для реализации [DI-контейнера](https://github.com/likip3/TelegaSharpProject/blob/master/TelegaSharpProject/Program.cs)
