export class SessionData {
    token;
    role;
    name;

    constructor(data = {}) {
        this.token = data.token;
        this.role = data.role;
        this.name = data.name;
    }

    static FromData(data) {
        if (data != null) {
            return new SessionData(data)
        }
        return new SessionData({token: null, role: null})
    }
}