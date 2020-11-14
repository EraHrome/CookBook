$(function () {

    var nextKeyWords = ["следующий", "следующая", "дальше", "продолжить"]
    var prevKeyWords = ["назад", "предыдущий"]
    var stopWords = ["стоп"]
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

        if (stopWords.indexOf(speech) !== -1) {
            if (!isTimeout) {
                stopTimer()
                event.preventDefault()
                isTimeout = !isTimeout
                setTimeout(() => { isTimeout = !isTimeout }, 1000)
            }
        }


        if (prevKeyWords.indexOf(speech) !== -1) {
            if (!isTimeout) {
                $('.btn')[0].click()
                event.preventDefault()
                isTimeout = !isTimeout
                setTimeout(() => { isTimeout = !isTimeout }, 1000)
            }
        }

        if (nextKeyWords.indexOf(speech) !== -1) {
            if (!isTimeout) {
                $('.btn')[1].click()
                event.preventDefault()
                isTimeout = !isTimeout
                setTimeout(() => { isTimeout = !isTimeout }, 1000)
            }
        }

    };
    SpeechRecognition.onend = function () {
        SpeechRecognition.start();
    };
    SpeechRecognition.start();

});