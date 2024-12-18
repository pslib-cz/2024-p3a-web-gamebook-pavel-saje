import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./styles/App.css";

import Game from "./pages/Game";
import Home from "./pages/Home";
import Admin from "./pages/Admin";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Home />,
    },
    {
        path: "/Game",
        element: <Game energy={-10} />,
        children: [
            {
                path: "/Game:id",
                element: <Game energy={-100} />,
            },
        ],
    },
    {
        path: "/SexAdmin",
        element: <Admin />,
    },
]);

const App: React.FC = () => {
    return (
        <>
            <RouterProvider router={router} />
        </>
    );
};

export default App;
