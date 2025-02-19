import { createBrowserRouter, RouterProvider } from "react-router-dom";
import "./styles/App.css";

import Game from "./pages/Game";
import Home from "./pages/Home";
import Admin from "./pages/Admin";
import Fight from "./pages/Fight";
import Ending from "./pages/End";
import Dialog from "./pages/Dialog";
import TradePage from "./pages/Trade";

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
        children: [
            {
                path: "/Fight:id",
                element: <Fight/>,
            }
        ]
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
    },
    {
        path: "/Dialog",
        element: <Dialog/>,
        children:[
            {
                path:"/Dialog:id",
                element: <Dialog/>
            }
        ]
    },
    {
        path: "/Trade",
        element: <TradePage/>,
        children: [
            {
                path: "/Trade:id",
                element: <TradePage/>
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
