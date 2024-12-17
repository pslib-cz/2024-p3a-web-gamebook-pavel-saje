import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import { useState } from 'react';
import './styles/App.css';

import Game from './pages/Game';
import { Location } from './types';

const router = createBrowserRouter([
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