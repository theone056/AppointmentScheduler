import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import { BrowserRouter } from 'react-router-dom'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import SignupForm from './_auth/forms/SignupForm.js'
import AuthLayout from './_auth/AuthLayout.js'
import RootLayout from './_root/RootLayout'
import { Home,homeLoader } from './_root/pages/index'
import SigninForm from './_auth/forms/SigninForm'




const router = createBrowserRouter([
    {
        element: <RootLayout />,
        children:[
            {
                path: '/',
                element: <Home/>,
                errorElement: <h1>404 not Found</h1>,
                loader: homeLoader
            },
        ]
    },
    {
        element : <AuthLayout/>,
        children: [{
            path: '/sign-up',
            element:<SignupForm />,
        },
        {
            path: '/sign-in',
            element:<SigninForm />,
        }]
    }
]);

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <RouterProvider router={router}/>
    </React.StrictMode>
)
