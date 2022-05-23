import "./App.css";
import Router from "./routes";
// theme
import ThemeConfig from './theme';
import { Provider, useSelector } from 'react-redux'
import { store } from "./store/store";
import { useEffect, useState } from "react";
import { Routes, useRoutes } from "react-router-dom";


function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  useEffect(() => {
    setIsLoggedIn(localStorage.getItem('token') != null)
  }, []);
  const routing = useRoutes(Router(isLoggedIn));
  return (
    <Provider  store={store}>
    <ThemeConfig>
      {routing}
    </ThemeConfig>
    </Provider>
  );
}

export default App;
