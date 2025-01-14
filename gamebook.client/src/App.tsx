import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./styles/App.css";

import Game from "./pages/Game";
import Home from "./pages/Home";
import Admin from "./pages/Admin";
import Fight from "./pages/Fight";
import Ending from "./pages/End";

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
    {
        path: "/Admin",
        element: <Admin />,
    },
    {
        path: "/Fight",
        element: <Fight />,
    },
    {
        path: "/Ending",
        element: <Ending/>,
        children: [
            {
                path: "/Ending:id",
                element: <Ending />
            }
        ]
    }
]);

const App: React.FC = () => {
    return (
        <>
            <RouterProvider router={router} />
        </>
    );
};

export default App;
