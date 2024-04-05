import React from 'react';
import { Outlet, Navigate } from 'react-router-dom';

const AuthLayout = () => {
    const isAuthenticated = false;

    return (
        <div>
            {isAuthenticated ? (
                <Navigate to="/" />) :
                (
                    <>
                        <section className="flex flex-1 justify-center items-center flex-col py-32">
                            <Outlet />
                        </section>
                    </>
                )
            }
        </div>
    )
}

export default AuthLayout;