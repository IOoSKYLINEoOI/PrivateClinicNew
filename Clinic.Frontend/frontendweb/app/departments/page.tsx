'use client';
import React, { useEffect, useState } from 'react';
import { Table, Button, Modal, Space, Popconfirm, message } from 'antd';
import { ColumnsType } from 'antd/es/table';
import { DepartmentRequest, DepartmentResponse } from '@/models/Department';
import { createDepartment, deleteDepartment, getDepartments, updateDepartment } from '@/utils/api';
import DepartmentForm from '@/components/departments/DepartmentForm';




const DepartmentsPage: React.FC = () => {
  const [departments, setDepartments] = useState<DepartmentResponse[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [isModalVisible, setIsModalVisible] = useState<boolean>(false);
  const [editingDepartment, setEditingDepartment] = useState<DepartmentResponse | null>(null);

  const fetchDepartments = async () => {
    setLoading(true);
    try {
      const data = await getDepartments();
      setDepartments(data);
    } catch (error) {
      message.error('Не удалось загрузить департаменты');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDepartments();
  }, []);

  const handleCreate = () => {
    setEditingDepartment(null);
    setIsModalVisible(true);
  };

  const handleEdit = (department: DepartmentResponse) => {
    setEditingDepartment(department);
    setIsModalVisible(true);
  };

  const handleDelete = async (id: string) => {
    try {
      await deleteDepartment(id);
      message.success('Департамент удален');
      fetchDepartments();
    } catch (error) {
      message.error('Не удалось удалить департамент');
    }
  };

  const handleSubmit = async (values: DepartmentRequest) => {
    try {
      if (editingDepartment) {
        await updateDepartment(editingDepartment.id, values);
        message.success('Департамент обновлен');
      } else {
        await createDepartment(values);
        message.success('Департамент создан');
      }
      setIsModalVisible(false);
      fetchDepartments();
    } catch (error) {
      message.error('Не удалось сохранить департамент');
    }
  };

  const columns: ColumnsType<DepartmentResponse> = [
    {
      title: 'Название',
      dataIndex: 'name',
      key: 'name',
    },
    {
      title: 'Описание',
      dataIndex: 'description',
      key: 'description',
    },
    {
      title: 'Страна',
      dataIndex: ['address', 'country'],
      key: 'country',
    },
    {
      title: 'Регион',
      dataIndex: ['address', 'region'],
      key: 'region',
    },
    {
      title: 'Город',
      dataIndex: ['address', 'city'],
      key: 'city',
    },
    {
      title: 'Улица',
      dataIndex: ['address', 'street'],
      key: 'street',
    },
    {
      title: 'Номер дома',
      dataIndex: ['address', 'houseNumber'],
      key: 'houseNumber',
    },
    {
      title: 'Номер квартиры',
      dataIndex: ['address', 'apartmentNumber'],
      key: 'apartmentNumber',
    },
    {
      title: 'Павильон',
      dataIndex: ['address', 'pavilion'],
      key: 'pavilion',
    },
    {
      title: 'Действия',
      key: 'actions',
      render: (_, record) => (
        <Space size="middle">
          <Button type="link" onClick={() => handleEdit(record)}>
            Редактировать
          </Button>
          <Popconfirm
            title="Вы уверены, что хотите удалить этот департамент?"
            onConfirm={() => handleDelete(record.id)}
            okText="Да"
            cancelText="Нет"
          >
            <Button type="link" danger>
              Удалить
            </Button>
          </Popconfirm>
        </Space>
      ),
    },
  ];

  return (
    <div style={{ padding: '24px' }}>
      <h1>Департаменты</h1>
      <Button type="primary" onClick={handleCreate} style={{ marginBottom: '16px' }}>
        Добавить департамент
      </Button>
      <Table
        dataSource={departments}
        columns={columns}
        rowKey="id"
        loading={loading}
      />

      <Modal
        title={editingDepartment ? 'Редактировать департамент' : 'Создать департамент'}
        visible={isModalVisible}
        onCancel={() => setIsModalVisible(false)}
        footer={null}
      >
        <DepartmentForm
          initialValues={
            editingDepartment
              ? {
                  name: editingDepartment.name,
                  description: editingDepartment.description,
                  address: {
                    country: editingDepartment.address.country,
                    region: editingDepartment.address.region,
                    city: editingDepartment.address.city,
                    street: editingDepartment.address.street,
                    houseNumber: editingDepartment.address.houseNumber,
                    apartmentNumber: editingDepartment.address.apartmentNumber,
                    description: editingDepartment.address.description,
                    pavilion: editingDepartment.address.pavilion,
                  },
                }
              : undefined
          }
          onSubmit={handleSubmit}
          onCancel={() => setIsModalVisible(false)}
        />
      </Modal>
    </div>
  );
};

export default DepartmentsPage;
