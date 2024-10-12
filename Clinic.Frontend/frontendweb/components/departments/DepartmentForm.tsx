// components/DepartmentForm.tsx
import React from 'react';
import { Form, Input, Button, Space } from 'antd';
import { DepartmentRequest } from '@/models/Department';


interface DepartmentFormProps {
  initialValues?: DepartmentRequest;
  onSubmit: (values: DepartmentRequest) => void;
  onCancel: () => void;
}

const DepartmentForm: React.FC<DepartmentFormProps> = ({ initialValues, onSubmit, onCancel }) => {
  const [form] = Form.useForm();

  const handleFinish = (values: any) => {
    onSubmit(values);
  };

  return (
    <Form
      form={form}
      layout="vertical"
      initialValues={initialValues}
      onFinish={handleFinish}
    >
      <Form.Item
        label="Название департамента"
        name="name"
        rules={[{ required: true, message: 'Пожалуйста, введите название департамента' }]}
      >
        <Input />
      </Form.Item>

      <Form.Item
        label="Описание"
        name="description"
      >
        <Input.TextArea />
      </Form.Item>

      <Form.Item label="Адрес">
        <Form.Item
          label="Страна"
          name={['address', 'country']}
          rules={[{ required: true, message: 'Пожалуйста, введите страну' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Регион"
          name={['address', 'region']}
          rules={[{ required: true, message: 'Пожалуйста, введите регион' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Город"
          name={['address', 'city']}
          rules={[{ required: true, message: 'Пожалуйста, введите город' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Улица"
          name={['address', 'street']}
          rules={[{ required: true, message: 'Пожалуйста, введите улицу' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Номер дома"
          name={['address', 'houseNumber']}
          rules={[{ required: true, message: 'Пожалуйста, введите номер дома' }]}
        >
          <Input type="number" />
        </Form.Item>
        <Form.Item
          label="Номер квартиры"
          name={['address', 'apartmentNumber']}
          rules={[{ required: true, message: 'Пожалуйста, введите номер квартиры' }]}
        >
          <Input type="number" />
        </Form.Item>
        <Form.Item
          label="Описание адреса"
          name={['address', 'description']}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Павильон"
          name={['address', 'pavilion']}
        >
          <Input />
        </Form.Item>
      </Form.Item>

      <Form.Item>
        <Space>
          <Button type="primary" htmlType="submit">
            Сохранить
          </Button>
          <Button onClick={onCancel}>
            Отмена
          </Button>
        </Space>
      </Form.Item>
    </Form>
  );
};

export default DepartmentForm;
