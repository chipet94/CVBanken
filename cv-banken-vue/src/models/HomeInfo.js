export class HomeInfo {
    content;
    slides;

    constructor(data = {}) {
        this.content = data.content;
        this.slides = data.slides;
    }

    static FromData(data) {
        if (data != null) {
            return new HomeInfo(data)
        }
        return new HomeInfo({content: null, slides: null})
    }
}