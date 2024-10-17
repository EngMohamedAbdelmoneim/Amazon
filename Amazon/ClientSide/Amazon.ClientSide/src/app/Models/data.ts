export class loginData
{
    constructor(displayName: string, token: string, email:string)
    {
        this.displayName = displayName;
        this.email = email;
        this.token = token
    }

    displayName: string;
    token: string;
    email: string;
}