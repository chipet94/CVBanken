export class SessionData {
    token;
    role;
    constructor(data = {}) {
        this.token = data.token;
        this.role = data.role;
    }
    static FromData(data) {
        if (data != null) {
            return new SessionData(data)
        }
        return new SessionData({token: null, role: null})
    }
}