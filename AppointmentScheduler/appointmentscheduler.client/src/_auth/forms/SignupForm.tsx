import React from 'react';
import { Button } from '../../components/ui/button';
import { z } from 'zod';
import { useForm } from 'react-hook-form';
import { zodResolver } from "@hookform/resolvers/zod";
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage
} from '@/components/ui/form'
import {Input } from '@/components/ui/input';
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
  } from "@/components/ui/card"
import { useOutletContext } from 'react-router-dom';
  

const formShema = z.object({
    username: z.string().min(2).max(50),
    firstname: z.string().min(2).max(50),
    lastname: z.string().min(2).max(50),
    email: z.string().email().min(5),
    password: z.string().min(4),
    confirmPassword: z.string().min(4),
    phoneNumber: z.string().refine((val) => !Number.isNaN(parseInt(val,10)),{
        message: "Expected number, received a string"
    })
}).refine((data)=> data.password === data.confirmPassword,{
    message:"Passwords do not match",
    path:["confirmPassword"]
})

const SignupForm = () => {

    const [setIsAuthenticated] = useOutletContext();

    const form = useForm<z.infer<typeof formShema>>({
        resolver: zodResolver(formShema),
        defaultValues:{
            username: "",
            firstname: "",
            lastname: "",
            email: "",
            password: "",
            confirmPassword:"",
            phoneNumber: ""
        }
    })

    const onSubmit = async (values: z.infer<typeof formShema>) => {
        const response = await fetch("/api/User/Register",{
            method: 'POST',
            headers:{
                "Content-Type": "application/json"
            },
            body: JSON.stringify(values)
        });

        const result = await response.json();
        if(result.succeeded)
        {
            setIsAuthenticated(true);
            localStorage.setItem("user", JSON.stringify({"authenticated":true}))
        }
    }

    return (
        <Card className='px-8'>
            <CardHeader>
                <CardTitle>Sign up</CardTitle>
            </CardHeader>
            <CardContent>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)}>
                        <FormField
                            control={form.control}
                            name='username' 
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>Username</FormLabel>
                                    <FormControl>
                                        <Input className='w-96' placeholder="username..." {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField 
                            control={form.control}
                            name='firstname'
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>First name</FormLabel>
                                    <FormControl>
                                        <Input className='w-96' placeholder='First name...' {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        <FormField 
                            control={form.control}
                            name='lastname'
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>Last name</FormLabel>
                                    <FormControl>
                                        <Input className='w-96' placeholder ="Last name..." {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        <FormField 
                            control={form.control}
                            name='phoneNumber'
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>Phone number</FormLabel>
                                    <FormControl>
                                        <Input className='w-96' placeholder="Phone number..." {...field}/>
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />
                        <FormField 
                            control={form.control}
                            name='email'
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>Email</FormLabel>
                                    <FormControl>
                                        <Input className='w-96' placeholder="Email..." {...field}/>
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        <FormField 
                            control={form.control}
                            name='password'
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>Password</FormLabel>
                                    <FormControl>
                                        <Input type='password' className='w-96' placeholder="Password..." {...field} />
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        <FormField 
                            control={form.control}
                            name='confirmPassword'
                            render={({field})=>(
                                <FormItem>
                                    <FormLabel>Confirm Password</FormLabel>
                                    <FormControl>
                                        <Input type='password' className='w-96' placeholder="Confirm Password..." {...field}/>
                                    </FormControl>
                                    <FormMessage />
                                </FormItem>
                            )}
                        />

                        <Button className='mt-10' type="submit">Submit</Button>
                    </form>
                </Form>
            </CardContent>
        </Card>
        
    )
}

export default SignupForm;