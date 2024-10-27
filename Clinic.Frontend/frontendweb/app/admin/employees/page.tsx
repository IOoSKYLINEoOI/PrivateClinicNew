'use client';
import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Input, message } from 'antd';
import { EmployeeResponse } from '@/models/Employee';
import { getEmployees, deleteEmployee } from '@/utils/api';
import styles from './page.module.css';
import EmployeeFormModal from '@/components/employees/EmployeeFormModal';

const EmployeeList: React.FC = () => {
  const [employees, setEmployees] = useState<EmployeeResponse[]>([]);
  const [filteredEmployees, setFilteredEmployees] = useState<EmployeeResponse[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState<EmployeeResponse | null>(null);
  const [searchId, setSearchId] = useState<string>('');

  useEffect(() => {
    fetchEmployees();
  }, []);

  const fetchEmployees = async () => {
    try {
      const data = await getEmployees();
      setEmployees(data);
      setFilteredEmployees(data);
    } catch (error) {
      message.error('Ошибка при загрузке сотрудников');
    }
  };

  const handleSearch = () => {
    if (searchId.trim() === '') {
      setFilteredEmployees(employees);
      return;
    }

    const result = employees.filter(employee => employee.id === searchId);
    if (result.length > 0) {
      setFilteredEmployees(result);
    } else {
      message.info('Сотрудник с таким ID не найден');
      setFilteredEmployees([]);
    }
  };

  const handleAdd = () => {
    setSelectedEmployee(null);
    setIsModalVisible(true);
  };

  const handleEdit = (employee: EmployeeResponse) => {
    setSelectedEmployee(employee);
    setIsModalVisible(true);
  };

  const handleDelete = async (id: string) => {
    try {
      await deleteEmployee(id);
      message.success('Сотрудник удален');
      fetchEmployees();
    } catch (error) {
      message.error('Ошибка при удалении сотрудника');
    }
  };

  const handleModalClose = () => {
    setIsModalVisible(false);
    fetchEmployees();
  };

  return (
    <div className={styles.container}>
      <div className={styles.searchSection}>
        <Input
          placeholder="Введите ID сотрудника"
          value={searchId}
          onChange={e => setSearchId(e.target.value)}
          style={{ width: 200, marginRight: 8 }}
        />
        <Button onClick={handleSearch} type="primary">
          Найти
        </Button>
      </div>

      <Button onClick={handleAdd} type="primary" style={{ margin: '16px 0' }}>
        Добавить сотрудника
      </Button>

      <Table
        dataSource={filteredEmployees}
        rowKey="id"
        columns={[
          { title: 'ID', dataIndex: 'id', key: 'id' },
          { title: 'Дата приема', dataIndex: 'HiringDate', key: 'HiringDate' },
          { title: 'Дата увольнения', dataIndex: 'DateOfDismissal', key: 'DateOfDismissal' },
          { title: 'Описание', dataIndex: 'Description', key: 'Description' },
          {
            title: 'Действия',
            key: 'actions',
            render: (text, record) => (
              <div>
                <Button onClick={() => handleEdit(record)} style={{ marginRight: 8 }}>
                  Редактировать
                </Button>
                <Button onClick={() => handleDelete(record.id)} danger>
                  Удалить
                </Button>
              </div>
            ),
          },
        ]}
      />

      <EmployeeFormModal
        visible={isModalVisible}
        onClose={handleModalClose}
        employee={selectedEmployee}
      />
    </div>
  );
};

export default EmployeeList;
