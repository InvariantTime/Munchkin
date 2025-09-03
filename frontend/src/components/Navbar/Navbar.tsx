import "./Navbar.css"
import Logo from "../../assets/logo.png"

interface Props
{
    IsLogged: boolean
}

export const Navbar = () =>
{
    return (
        <div className="h-[60px] flex items-center justify-between bg-primary px-4">
            <a className="flex flex-row h-full items-center gap-2" href="/">
 
                <img src={Logo} className="noselect"></img>
                
                <div className="text-white text-3xl noselect">
                    MUNCHKIN
                </div>
               {/*LOGO*/}
            </a>

            <div>
                <LoginButton/>
            </div>
        </div>
    );
}

const LoginButton = () =>
{
    return (
        <a className="loginButton" href="/login">
            <div className="text-center w-full text-xl noselect">
                Login
            </div>
        </a>
    );
}