import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card';
import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import React from 'react';
import { Home as HomeICon } from 'lucide-react';
import { useLoaderData } from 'react-router-dom';
import DataTable from './Components/DataTable';
import { any } from 'zod';
import { ApiResponse, Appointments } from '@/types/Types';


export const homeLoader = async () : Promise<Appointments[]> =>  {
    const res = await fetch("/api/Appointment/GetAll");
    const values: ApiResponse<Appointments[]> = await res.json().then((resp:ApiResponse<Appointments[]>)=> {
        return resp;
    })

    if(values.success)
    {
        return values.data
    }

    return [];
}


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