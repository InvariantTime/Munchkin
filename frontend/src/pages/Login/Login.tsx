export const LoginPage = () =>
{
    return (
        <div className="flex h-full justify-center items-center">
            <div className="w-full max-w-lg text-white pb-10
             bg-primary rounded-lg shadow-shadow">

                <h3 className="text-3xl pt-5 text-center">Register</h3>
                <form>
                    <div className="pb-5">
                        <label className="block text-2xl pl-4">Munchkin name</label>

                        <div className="text-center pt-2">
                            <input className="h-10 text-xl w-[60%] bg-shadow text-center"/>
                        </div>
                    </div>

                    <div className="pb-5">
                        <label className="block text-2xl pl-4">Your password</label>
                        
                        <div className="text-center pt-2">
                            <input type="password" 
                                className="h-10 text-xl bg-shadow w-[60%] text-center"/>
                        </div>
                    </div>

                    <div className="pb-5">
                        <label className="block text-2xl pl-4">Your password again...</label>
                        
                        <div className="text-center pt-2">
                            <input type="password"
                                className="h-10 text-xl bg-shadow w-[60%] text-center"/>
                        </div>
                    </div>

                    <div className="w-full">
                        <button type="submit"
                            className="right-0 w-[30px] h-[40px]"/>
                    </div>
                </form>
            </div>
        </div>
    );
}

const LoginForm = () =>
{
    return (
        <div className="w-[50%] h-[60%] bg-primary shadow-md shadow-shadow">
            <form>

            </form>
        </div>
    );
}

const SigninForm = () =>
{
    return (
        <div>
            <form>
                
            </form>
        </div>
    );
}