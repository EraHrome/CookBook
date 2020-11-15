var activeTimer
var timeCount

function stopTimer() {
    clearInterval(activeTimer)
}

$(function () {

    $(document).on('click', '.ingredients__btn.start.active', function () {
        $('.ingredients__btn').removeClass('active')
        $('.ingredients__btn.stop').addClass('active')
        $('.ingredients__btn.restart').addClass('active')
        activeTimer = startNewTimer(timeCount)
    });

    $(document).on('click', '.ingredients__btn.stop.active', function () {
        $('.ingredients__btn').removeClass('active')
        $('.ingredients__btn.restart').addClass('active')
        $('.ingredients__btn.continue').addClass('active')
        stopTimer();
    });

    $(document).on('click', '.ingredients__btn.restart.active', function () {
        $('.ingredients__btn').removeClass('active')
        $('.ingredients__btn.stop').addClass('active')
        $('.ingredients__btn.restart').addClass('active')
        $('#countdownA').replaceWith('<div class="countdown-bar" id="countdownA"><div></div><div></div></div>')
        stopTimer()
        activeTimer = startNewTimer(timeCount)
    });

    $(document).on('click', '.ingredients__btn.continue.active', function () {
        $('.ingredients__btn').removeClass('active')
        $('.ingredients__btn.stop').addClass('active')
        $('.ingredients__btn.restart').addClass('active')
        var arr = $('.countdown-bar div span').text().split(':')
        var seconds = parseInt(arr[2])
        var minutes = parseInt(arr[1])
        var hours = parseInt(arr[0])
        var totalSeconds = hours * 60 * 60 + minutes * 60 + seconds
        $('#countdownA').replaceWith('<div class="countdown-bar" id="countdownA"><div></div><div></div></div>')
        activeTimer = startNewTimer(totalSeconds)
    });

    $(document).on('click', '.btn-timeline-controls', function () {
        $('.ingredients__btn:not(.start)').removeClass('active')
    });

    var isActiveTimer = false
    var dataNodes = $('.checkpoint-desc')
    var newSteps = []
    dataNodes.each(function (index) {
        newSteps.push({ "label": "test", "content": $(this).text(), "timeCount": $(this).data('value') })
    })

    var app = new Vue({
        el: '#app',
        data: {
            currentStep: null,
            steps: newSteps
        },
        methods: {
            nextStep(next = true) {
                const steps = this.steps
                const currentStep = this.currentStep
                const currentIndex = steps.indexOf(currentStep)

                if (next) {
                    if (steps[currentIndex] && steps[currentIndex].timeCount) {
                        $('.ingredients__btn.start').addClass('active');
                        timeCount = steps[currentIndex].timeCount
                        //activeTimer = startNewTimer(steps[currentIndex].timeCount)
                        isActiveTimer = true
                    }
                    else if (isActiveTimer) {
                        $('.ingredients__btn.start').removeClass('active');
                        isActiveTimer = false
                        stopTimer()
                        $('#countdownA').replaceWith('<div class="countdown-bar" id="countdownA"><div></div><div></div></div>')
                    }
                }
                else {
                    if (steps[currentIndex - 2] && steps[currentIndex - 2].timeCount) {
                        $('.ingredients__btn.start').addClass('active');
                        timeCount = steps[currentIndex].timeCount
                        //activeTimer = startNewTimer(steps[currentIndex].timeCount)
                        isActiveTimer = true
                    }
                    else if (isActiveTimer) {
                        $('.ingredients__btn.start').removeClass('active');
                        isActiveTimer = false
                        stopTimer()
                        $('#countdownA').replaceWith('<div class="countdown-bar" id="countdownA"><div></div><div></div></div>')
                    }
                }

                // handle back
                if (!next) {

                    if (currentStep && currentStep.label === 'complete') {
                        return this.currentStep = steps[steps.length - 1]
                    }

                    if (steps[currentIndex - 1]) {
                        return this.currentStep = steps[currentIndex - 1]
                    }

                    return this.currentStep = { "label": "start" }
                }

                // handle next
                if (this.currentStep && this.currentStep.label === 'complete') {
                    return this.currentStep = { "label": "start" }
                }

                if (steps[currentIndex + 1]) {
                    return this.currentStep = steps[currentIndex + 1]
                }

                this.currentStep = { "label": "complete" }
            },
            stepClasses(index) {
                let result = `progress__step progress__step--${index + 1} `
                if (this.currentStep && this.currentStep.label === 'complete' ||
                    index < this.steps.indexOf(this.currentStep)) {
                    return result += 'progress__step--complete'
                }
                if (index === this.steps.indexOf(this.currentStep)) {
                    return result += 'progress__step--active'
                }
                return result
            }
        },
        computed: {
            progressClasses() {
                let result = 'progress '
                if (this.currentStep && this.currentStep.label === 'complete') {
                    return result += 'progress--complete'
                }
                return result += `progress--${this.steps.indexOf(this.currentStep) + 1}`
            }
        }
    })

});

$("#start-cooking").click(function () {
    $(".controls__container-watch").hide();
    $(".controls__container-cooking").show();
    $("#start-cooking").hide();
});