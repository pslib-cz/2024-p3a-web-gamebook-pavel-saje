import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import './styles/App.css';

import Game from './pages/Game';
import Home from './pages/Home';

const router = createBrowserRouter([
  {
    path: "/",
    element: <Home />,
  },
  {
    path: "/Game",
    element: <Game />,
    children: [
      {
        path: "/Game:id",
        element: <Game />,
      },
    ],
  },
]);



const App: React.FC = () => {

    return (
      <>
        <RouterProvider router={router} />
      </>
    );
}

export default App;