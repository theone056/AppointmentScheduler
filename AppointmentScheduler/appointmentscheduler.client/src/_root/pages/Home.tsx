import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card';
import { Table, TableBody, TableCaption, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table';
import React from 'react';
import { Home as HomeICon } from 'lucide-react';
import { useLoaderData } from 'react-router-dom';


type Appointments = {
    id:string,
    date: string,
    title: string,
    description: string,
    attendees: string[],
    createdBy: string
}

export const homeLoader = async () : Promise<Appointments[]> =>  {
    const res = await fetch("/api/Appointment/GetAll")
    return res.json();
}


const Home = () => {
    const appointments = useLoaderData() as Appointments[];

    return (
        <>
            <h1 className='font-bold pb-9 flex'><HomeICon />Home</h1>
            <div className='flex'>
                <Card>
                    <CardHeader>
                        <CardTitle>Todays Appointment</CardTitle>
                    </CardHeader>
                    <CardContent>
                        <Table>
                            <TableHeader>
                                <TableRow>
                                    <TableHead>Date</TableHead>
                                    <TableHead>Title</TableHead>
                                    <TableHead>Description</TableHead>
                                    <TableHead>Attendees</TableHead>
                                    <TableHead>Created By</TableHead>
                                </TableRow>
                            </TableHeader>
                            <TableBody>
                                {appointments.map(appoint => (
                                    <TableRow key={appoint.id}>
                                        <TableCell>{new Date(appoint.date).toUTCString()}</TableCell>
                                        <TableCell>{appoint.title}</TableCell>
                                        <TableCell>{appoint.description}</TableCell>
                                        <TableCell>{appoint.attendees.join(', ')}</TableCell>
                                        <TableCell>{appoint.createdBy}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                        
                    </CardContent>
                    <CardFooter>

                    </CardFooter>
                </Card>
            </div>
        </>
    )
}

export default Home;