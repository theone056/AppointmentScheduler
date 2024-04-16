import { ApiResponse, Appointments } from "@/models/Types";

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