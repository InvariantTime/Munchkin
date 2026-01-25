import { Background } from './components/Background/Background';
import { Navbar } from './components/Navbar/Navbar';
import { Lobby } from './pages/Lobby/Lobby';
import { LoginPage } from './pages/Login/Login';
import { GameSession } from './pages/Session/GameSession';

function App() {
  return (
    <>
      <Background>
          <GameSession/>
      </Background>
    </>
  );
}

export default App;
