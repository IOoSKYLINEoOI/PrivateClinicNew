import Card from "antd/es/card/Card"
import Button from "antd/es/button/button"
import { CardEmployee } from "./CardEmployee";

interface Props {
    employees: Employee[];
    handleDelete: (id: string) => void;
    handleOpen: (employee: Employee) => void;
}


export const Employees = ({employees, handleDelete, handleOpen} : Props) =>{
    return (
        <div className="cards">
            {employees.map((employee : Employee) => (
                <Card 
                key={employee.id}
                title ={<CardEmployee name={employee.id}/>}
                bordered = {false}
                >
                    <p>{employee.description}</p>
                    <div className="card_buttoms">
                        <Button 
                            onClick={() => handleOpen(employee)}
                            style = {{flex : 1}}
                        >
                            Редактировать
                        </Button>
                        <Button
                            onClick={() => handleDelete(employee.id)}
                            danger
                            style = {{flex : 1}}
                        >
                            Удалить
                        </Button>
                    </div>
                </Card>
            ))}
        </div>
    )
}