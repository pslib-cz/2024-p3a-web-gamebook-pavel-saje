import { Link } from "react-router-dom";

const Home = () => {
    return (
        <>
            <h1>Hlavni Menu</h1>
            <p>
                <Link to="/Game">Play</Link>
            </p>
            <p>
                <Link to="/Admin">Admin</Link>
            </p>
            <p onClick={() => alert("YOU ARE KOKOT🐒🐒🐒AND NIGGA🐒🐒🐒")}>
                Jsem kokot
            </p>
        </>
    );
};
export default Home;
