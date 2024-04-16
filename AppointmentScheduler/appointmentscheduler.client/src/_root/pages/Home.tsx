import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card';
import React from 'react';
import { Home as HomeICon } from 'lucide-react';
import { useLoaderData } from 'react-router-dom';
import DataTable from './Components/DataTable';
import { Appointments } from '@/types/Types';

const Home = () => {
    const appointments = useLoaderData() as Appointments[];
    const header: string[] = ["Date","Title", "Description", "Attendees", "Created By"]
    return (
        <>
            <h1 className='font-bold pb-9 flex'><HomeICon />Home</h1>
            <div className='flex gap-3'>
                <Card>
                    <CardHeader>
                        <CardTitle>Todays Appointment</CardTitle>
                    </CardHeader>
                    <CardContent>
                       <DataTable  headerData={header} data={appointments} />
                    </CardContent>
                </Card>
                <Card>
                    <CardHeader>
                        <CardTitle>Todays Appointment</CardTitle>
                    </CardHeader>
                    <CardContent>
                       <DataTable  headerData={header} data={appointments} />
                    </CardContent>
                </Card>
            </div>
        </>
    )
}

export default Home;