$(function () {       

	   var app = new Vue({
            el: '#app',
            data: {
                currentStep: null,
                steps: [
                    { "label": "Этап 1", "content": "Срежьте с куска мяса лишний жир, если он, по-вашему, есть. Разрежьте стейк на 2 равные части, сохраняя толщину. Растолките перец в ступке не очень мелко. Натрите мясо с двух сторон (не там, где разрез) солью, вдавите пальцами перец. Дайте мясу полежать 15–20 мин." },
                    { "label": "Этап 2", "content": "В хорошо разогретой на сильном огне сковороде с толстым дном растопите в оливковом сливочное масло. Уложите стейки на сковороду, жарьте 3 мин. Переверните и жарьте еще 2 мин. Снимите сковороду с огня и дайте постоять 5 мин. Затем верните сковороду на огонь и жарьте еще по 1–3 мин. на каждой стороне, до желаемой степени прожарки. Переложите мясо на подогретые тарелки и накройте куском фольги, не заворачивая края." },
                    { "label": "Этап 3", "content": "Пока мясо отдыхает, сделайте соус. Нарежьте очень мелко шалот, положите его на сковороду, где жарилось мясо. Обжарьте на среднем огне 2 мин. Наклоните сковороду на огне и, держась от нее подальше, влейте коньяк. Он должен загореться (если у вас электрическая плита или коньяк не загорелся от газа, подожгите его прямо в сковороде длинной спичкой). Дайте алкоголю прогореть." },
                    { "label": "Этап 4", "content": "Добавьте теплый бульон, готовьте на сильном огне, помешивая, 1 мин. Добавьте масло, снимите с огня и подайте к стейкам." }
                ]
            },
            methods: {
                nextStep(next = true) {
                    const steps = this.steps
                    const currentStep = this.currentStep
                    const currentIndex = steps.indexOf(currentStep)

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