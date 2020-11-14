$(function () {

    var nextKeyWords = ["следующий", "следующая", "дальше", "продолжить"]
    var prevKeyWords = ["назад", "предыдущий"]
    var isTimeout = false

    if (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition) {
        console.log('Браузер поддерживает данную технологию');
    } else {
        console.log('Не поддерживается данным браузером');
    }
    var SpeechRecognition = new (window.SpeechRecognition || window.webkitSpeechRecognition || window.mozSpeechRecognition || window.msSpeechRecognition)();
    SpeechRecognition.lang = "ru-RU";
    SpeechRecognition.interimResults = true;
    SpeechRecognition.onresult = function (event) {

        var speech = event.results[0][0].transcript;

        if (prevKeyWords.indexOf(speech) !== -1) {
            if (!isTimeout) {
                console.log("Событие переключения назад")
                $('.btn')[0].click()
                isTimeout = !isTimeout
                setTimeout(() => { isTimeout = !isTimeout }, 750)
            }
        }

        if (nextKeyWords.indexOf(speech) !== -1) {
            if (!isTimeout) {
                console.log("Событие переключения вперед")
                $('.btn')[1].click()
                isTimeout = !isTimeout
                setTimeout(() => { isTimeout = !isTimeout }, 750)
            }
        }
    };
    SpeechRecognition.onend = function () {
        SpeechRecognition.start();
    };
    SpeechRecognition.start();

});