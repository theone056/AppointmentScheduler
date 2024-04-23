import React from 'react'
import DataTable from './Components/DataTable'

const Appointments = () => {
    const header: string[] = ["Date","Title", "Description", "Attendees", "Created By"]
  return (
    <>
       <h1 className='font-bold pb-9 flex'>Appoinment</h1> 
       <div className='container mx-auto'>
          <DataTable headerData={header} data={[]} />
       </div>
    </>
  )
}

export default Appointments