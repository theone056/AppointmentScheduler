import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from '@/components/ui/table'
import React from 'react'
import { Appointments } from '../Home'

const DataTable = (props:{headerData : string[] , data : Appointments[]}) => {
  return (
    <>
        <Table>
            <TableHeader>
            <TableRow>
                {props.headerData.map((header:string)=>(
                    <TableHead key={header}>{header}</TableHead>
                ))}
            </TableRow>
            </TableHeader>
            <TableBody>
            {props.data.map((appoint:Appointments) => (
                <TableRow key={appoint.id}>
                    <TableCell>{new Date(appoint.date).toLocaleString()}</TableCell>
                    <TableCell>{appoint.title}</TableCell>
                    <TableCell>{appoint.description}</TableCell>
                    <TableCell>{appoint.attendees.join(', ')}</TableCell>
                    <TableCell>{appoint.createdBy}</TableCell>
                </TableRow>
            ))}
            </TableBody>
        </Table>
    </>
  )
}

export default DataTable