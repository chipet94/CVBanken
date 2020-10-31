import store from '@/store'

export const ValidatePathRules = (to, from, next) => {
    let user = store.getters["auth/getSession"]
    if (to.matched.some(record => record.meta.requireAuth)) // om sidan kräver auth.
        if (!user.token)                   //om ingen userData finns, skicka till lgoin.
            return next({
                path: '/login',
                params: {nextUrl: to.fullPath}
            })
        else if (to.matched.some(record => record.meta.requireAdmin)) // om sidan kräver admin      
            if (user.role === "Admin")
                return next()
            else
                return next(from)   //stanna om du inte är admin. 
        else
            return next()
    //else if (to.matched.some(record => record.meta.guest))   // om sidan är för gäster
    //inga krav
    return next()
}