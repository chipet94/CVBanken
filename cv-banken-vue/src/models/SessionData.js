export class SessionData {
    token;
    role;
    name;
    url;

    constructor(data = {}) {
        this.token = data.token;
        this.role = data.role;
        this.name = data.name;
        this.url = data.url;
    }

    static FromData(data) {
        if (data != null) {
            return new SessionData(data)
        }
        return new SessionData({token: null, role: null})
    }
}