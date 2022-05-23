import { Navigate, useRoutes } from 'react-router-dom';
// layouts
import DashboardLayout from './layouts/DashboardLayout';
import Autores from './pages/Autores';
import Login from './pages/auth/Login';
import Editoriales from './pages/Editoriales';
import Libros from './pages/Libros';

// ----------------------------------------------------------------------

export default function Router(isLoggedIn) {
  return [
    {
      path: '/',
      element: isLoggedIn ? <DashboardLayout /> :  <Navigate to="/login" />,
      children: [
        { path: '/', element: <Navigate to="/libros" replace /> },
        { path: 'autores', element: <Autores /> },
        { path: 'editoriales', element: <Editoriales /> },
        { path: 'libros', element: <Libros /> },
      ]
    },
    {
      path: '/login',
      element: !isLoggedIn ? <Login /> :  <Navigate to="/" />,
    },
    { path: '*', element: <Navigate to="/login" replace /> }
  ];
}
