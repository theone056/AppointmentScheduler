import React, { useState } from 'react';
import { Outlet, Navigate } from 'react-router-dom';

const AuthLayout = () => {
    const [isAuthenticated,setIsAuthenticated] = useState(false);

    useState(()=>{
        const auth = JSON.parse(localStorage.getItem("user"));
        if(auth)
        {
            setIsAuthenticated(auth.authenticated)
        }
    }, [isAuthenticated])

    return (
        <div>
            {isAuthenticated ? (
                <Navigate to="/" />) :
                (
                    <>
                        <section className="flex flex-1 justify-center items-center flex-col py-32">
                            <Outlet context={[setIsAuthenticated]} />
                        </section>
                    </>
                )
            }
        </div>
    )
}

export default AuthLayout;