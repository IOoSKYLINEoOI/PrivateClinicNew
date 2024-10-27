'use client';
import React, { useEffect } from 'react';
import { Modal, Form, Input, Button, message } from 'antd';
import { DepartmentRequest, DepartmentResponse } from '@/models/Department';
import { createDepartment, updateDepartment } from '@/utils/api';

interface DepartmentFormModalProps {
  visible: boolean;
  onClose: () => void;
  department?: DepartmentResponse | null;
}

const DepartmentFormModal: React.FC<DepartmentFormModalProps> = ({ visible, onClose, department }) => {
  const [form] = Form.useForm();

  useEffect(() => {
    if (department) {
      form.setFieldsValue({
        name: department.name,
        description: department.description,
        address: department.address,
      });
    } else {
      form.resetFields();
    }
  }, [department, form]);

  const onFinish = async (values: DepartmentRequest) => {
    try {
      if (department) {
        await updateDepartment(department.id, values);
        message.success('Департамент обновлен');
      } else {
        await createDepartment(values);
        message.success('Департамент добавлен');
      }
      onClose();
    } catch (error) {
      message.error('Ошибка при сохранении департамента');
    }
  };

  return (
    <Modal
      title={department ? 'Редактировать департамент' : 'Добавить департамент'}
      visible={visible}
      onCancel={onClose}
      footer={null}
    >
      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item name="name" label="Название" rules={[{ required: true, message: 'Укажите название' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="description" label="Описание">
          <Input.TextArea rows={4} />
        </Form.Item>

        {/* Поля адреса */}
        <Form.Item name={['address', 'country']} label="Страна" rules={[{ required: true, message: 'Укажите страну' }]}>
          <Input />
        </Form.Item>
        <Form.Item name={['address', 'region']} label="Регион" rules={[{ required: true, message: 'Укажите регион' }]}>
          <Input />
        </Form.Item>
        <Form.Item name={['address', 'city']} label="Город" rules={[{ required: true, message: 'Укажите город' }]}>
          <Input />
        </Form.Item>
        <Form.Item name={['address', 'street']} label="Улица" rules={[{ required: true, message: 'Укажите улицу' }]}>
          <Input />
        </Form.Item>
        <Form.Item name={['address', 'houseNumber']} label="Номер дома" rules={[{ required: true, message: 'Укажите номер дома' }]}>
          <Input type="number" />
        </Form.Item>
        <Form.Item name={['address', 'apartmentNumber']} label="Номер квартиры">
          <Input type="number" />
        </Form.Item>
        <Form.Item name={['address', 'description']} label="Описание адреса">
          <Input.TextArea rows={2} />
        </Form.Item>
        <Form.Item name={['address', 'pavilion']} label="Павильон">
          <Input />
        </Form.Item>

        <Form.Item>
          <Button type="primary" htmlType="submit">
            {department ? 'Сохранить изменения' : 'Добавить департамент'}
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default DepartmentFormModal;
