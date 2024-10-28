"use client";
import React, { useState } from 'react';
import { Table, Button, Modal, Form, Input, Space, Typography } from 'antd';
import styles from './AdminPanel.module.css';

const { Title } = Typography;

interface UserData {
  key: string;
  name: string;
  email: string;
  role: string;
}

const AdminPanel: React.FC = () => {
  const [data, setData] = useState<UserData[]>([
    { key: '1', name: 'Иван Иванов', email: 'ivanov@mail.com', role: 'User' },
    { key: '2', name: 'Петр Петров', email: 'petrov@mail.com', role: 'Admin' },
  ]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentUser, setCurrentUser] = useState<UserData | null>(null);
  const [form] = Form.useForm();

  const showEditModal = (user: UserData) => {
    setCurrentUser(user);
    form.setFieldsValue(user);
    setIsModalOpen(true);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
    form.resetFields();
  };

  const handleSave = () => {
    form.validateFields().then(values => {
      if (currentUser) {
        setData(data.map(user => (user.key === currentUser.key ? { ...user, ...values } : user)));
      }
      setIsModalOpen(false);
      form.resetFields();
    });
  };

  const handleDelete = (key: string) => {
    setData(data.filter(user => user.key !== key));
  };

  const columns = [
    { title: 'Имя', dataIndex: 'name', key: 'name' },
    { title: 'Email', dataIndex: 'email', key: 'email' },
    { title: 'Роль', dataIndex: 'role', key: 'role' },
    {
      title: 'Действия',
      key: 'action',
      render: (_: any, record: UserData) => (
        <Space size="middle">
          <Button type="link" onClick={() => showEditModal(record)}>Редактировать</Button>
          <Button type="link" danger onClick={() => handleDelete(record.key)}>Удалить</Button>
        </Space>
      ),
    },
  ];

  return (
    <div className={styles.adminPanel}>
      <Title level={4}>Список пользователей</Title>
      <Table columns={columns} dataSource={data} />

      <Modal
        title="Редактировать пользователя"
        open={isModalOpen}
        onCancel={handleCancel}
        onOk={handleSave}
      >
        <Form form={form} layout="vertical">
          <Form.Item name="name" label="Имя" rules={[{ required: true, message: 'Введите имя' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="email" label="Email" rules={[{ required: true, message: 'Введите email' }]}>
            <Input />
          </Form.Item>
          <Form.Item name="role" label="Роль" rules={[{ required: true, message: 'Введите роль' }]}>
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
};

export default AdminPanel;
