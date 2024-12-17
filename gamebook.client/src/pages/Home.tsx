import { Link } from "react-router-dom"

const Home = () => {
    return(
        <>
        <p><Link to="/Game">chci hrát</Link></p>
        <p onClick={() => alert("Já to věděl 😉")}>Jsem kokot</p>
        </>
    )
}
export default Home