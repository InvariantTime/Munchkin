import { Background } from './components/Background/Background';
import { Navbar } from './components/Navbar/Navbar';
import { Lobby } from './pages/Lobby/Lobby';
import { LoginPage } from './pages/Login/Login';

function App() {
  return (
    <>
      <Background>
          <LoginPage/>
      </Background>
    </>
  );
}

export default App;
