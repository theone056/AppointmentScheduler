import React from 'react';
import { Link, NavLink, Outlet } from 'react-router-dom';

const RootLayout = () => {
    return (
        <>
            <div className='flex'>
                <div className='w-1/5 bg-black text-white min-h-screen px-10'>
                    <h1 className="text-lg py-5"><NavLink to="/">Appointment</NavLink></h1>
                    <ul className='flex flex-col gap-5 pt-5'>
                        <li><NavLink to="/">Home</NavLink></li>
                        <li><NavLink to='/Appointents'>Appointments</NavLink></li>
                        <li><NavLink to='/Calendar'>Calendar</NavLink></li>
                    </ul>
                </div>
                <section className='container-fluid w-100 p-5'>
                    <Outlet />
                </section>
            </div>
        </>
    )
}

export default RootLayout;