'use client';
import React, { useEffect, useState } from 'react';
import { Collapse, Table, message } from 'antd';
import { DepartmentResponse } from '@/models/Department';
import { EmployeeDepartmentResponse } from '@/models/EmployeeDepartment';
import { getDepartments, getEmployeesDepartments } from '@/utils/api';

const { Panel } = Collapse;

const DepartmentsEmployees: React.FC = () => {
  const [departments, setDepartments] = useState<DepartmentResponse[]>([]);
  const [employeesMap, setEmployeesMap] = useState<Record<string, EmployeeDepartmentResponse[]>>({});
  const [activeKey, setActiveKey] = useState<string | string[]>('');

  useEffect(() => {
    const fetchDepartments = async () => {
      try {
        const departmentsData = await getDepartments();
        setDepartments(departmentsData);
      } catch (error) {
        message.error("Ошибка при загрузке департаментов");
      }
    };

    fetchDepartments();
  }, []);

  const fetchEmployeesByDepartment = async (departmentId: string) => {
    try {
      const employeesData = await getEmployeesDepartments();
      const filteredEmployees = employeesData.filter(emp => emp.DepartmentId === departmentId);
      setEmployeesMap((prev) => ({
        ...prev,
        [departmentId]: filteredEmployees,
      }));
    } catch (error) {
      message.error("Ошибка при загрузке сотрудников");
    }
  };

  const handlePanelChange = (keys: string | string[]) => {
    setActiveKey(keys);
    if (typeof keys === 'string' && !employeesMap[keys]) {
      fetchEmployeesByDepartment(keys);
    } else if (Array.isArray(keys)) {
      keys.forEach((key) => {
        if (!employeesMap[key]) {
          fetchEmployeesByDepartment(key);
        }
      });
    }
  };

  return (
    <Collapse activeKey={activeKey} onChange={handlePanelChange}>
      {departments.map(department => (
        <Panel header={department.name} key={department.id}>
          <p>{department.description}</p>
          <Table
            dataSource={employeesMap[department.id]}
            columns={[
              { title: 'ID', dataIndex: 'EmployeeId', key: 'EmployeeId' },
              { title: 'Имя', dataIndex: 'Name', key: 'Name' }, // Предположим, что это поле есть в EmployeeDepartmentResponse
              { title: 'Должность', dataIndex: 'PositionId', key: 'PositionId' },
            ]}
            pagination={false}
            rowKey="EmployeeId"
          />
        </Panel>
      ))}
    </Collapse>
  );
};

export default DepartmentsEmployees;
