'use client';
import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Input, message } from 'antd';
import { DepartmentResponse } from '@/models/Department';
import { getDepartments, deleteDepartment } from '@/utils/api';
import styles from './page.module.css';
import DepartmentFormModal from '@/components/departments/DepartmentFormModal';

const DepartmentList: React.FC = () => {
  const [departments, setDepartments] = useState<DepartmentResponse[]>([]);
  const [filteredDepartments, setFilteredDepartments] = useState<DepartmentResponse[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [selectedDepartment, setSelectedDepartment] = useState<DepartmentResponse | null>(null);
  const [searchId, setSearchId] = useState<string>('');

  useEffect(() => {
    fetchDepartments();
  }, []);

  const fetchDepartments = async () => {
    try {
      const data = await getDepartments();
      setDepartments(data);
      setFilteredDepartments(data);
    } catch (error) {
      message.error('Ошибка при загрузке департаментов');
    }
  };

  const handleSearch = () => {
    if (searchId.trim() === '') {
      setFilteredDepartments(departments);
      return;
    }

    const result = departments.filter(department => department.id === searchId);
    if (result.length > 0) {
      setFilteredDepartments(result);
    } else {
      message.info('Департамент с таким ID не найден');
      setFilteredDepartments([]);
    }
  };

  const handleAdd = () => {
    setSelectedDepartment(null);
    setIsModalVisible(true);
  };

  const handleEdit = (department: DepartmentResponse) => {
    setSelectedDepartment(department);
    setIsModalVisible(true);
  };

  const handleDelete = async (id: string) => {
    try {
      await deleteDepartment(id);
      message.success('Департамент удален');
      fetchDepartments();
    } catch (error) {
      message.error('Ошибка при удалении департамента');
    }
  };

  const handleModalClose = () => {
    setIsModalVisible(false);
    fetchDepartments();
  };

  return (
    <div className={styles.container}>
      <div className={styles.searchSection}>
        <Input
          placeholder="Введите ID департамента"
          value={searchId}
          onChange={e => setSearchId(e.target.value)}
          style={{ width: 200, marginRight: 8 }}
        />
        <Button onClick={handleSearch} type="primary">
          Найти
        </Button>
      </div>

      <Button onClick={handleAdd} type="primary" style={{ margin: '16px 0' }}>
        Добавить департамент
      </Button>

      <Table
        dataSource={filteredDepartments}
        rowKey="id"
        columns={[
          { title: 'ID', dataIndex: 'id', key: 'id' },
          { title: 'Название', dataIndex: 'name', key: 'name' },
          { title: 'Описание', dataIndex: 'description', key: 'description' },
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

      <DepartmentFormModal
        visible={isModalVisible}
        onClose={handleModalClose}
        department={selectedDepartment}
      />
    </div>
  );
};

export default DepartmentList;
