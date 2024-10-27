import React, { useEffect } from 'react';
import { Modal, Form, Input, Button, Select, message } from 'antd';
import { createEmployeesDepartments, updateEmployeesDepartments } from '@/utils/api';
import { EmployeeDepartmentRequest, EmployeeDepartmentResponse } from '@/models/EmployeeDepartment';

interface DepartmentEmployeeFormModalProps {
  visible: boolean;
  onClose: () => void;
  onSave: () => void;
  employeeDepartment?: EmployeeDepartmentResponse | null;
}

const DepartmentEmployeeFormModal: React.FC<DepartmentEmployeeFormModalProps> = ({
  visible,
  onClose,
  onSave,
  employeeDepartment,
}) => {
  const [form] = Form.useForm();

  useEffect(() => {
    if (employeeDepartment) {
      form.setFieldsValue(employeeDepartment);
    } else {
      form.resetFields();
    }
  }, [employeeDepartment, form]);

  const onFinish = async (values: EmployeeDepartmentRequest) => {
    try {
      if (employeeDepartment) {
        await updateEmployeesDepartments(employeeDepartment.EmployeeId, values);
        message.success('Связь обновлена');
      } else {
        await createEmployeesDepartments(values);
        message.success('Связь добавлена');
      }
      onSave();
      onClose();
    } catch (error) {
      message.error('Ошибка при сохранении связи');
    }
  };

  return (
    <Modal
      title={employeeDepartment ? 'Редактировать связь' : 'Добавить связь'}
      visible={visible}
      onCancel={onClose}
      footer={null}
    >
      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item name="EmployeeId" label="ID Сотрудника" rules={[{ required: true, message: 'Введите ID сотрудника' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="DepartmentId" label="ID Департамента" rules={[{ required: true, message: 'Введите ID департамента' }]}>
          <Input />
        </Form.Item>
        <Form.Item name="PositionId" label="Должность" rules={[{ required: true, message: 'Введите должность' }]}>
          <Select>
            <Select.Option value={1}>Врач</Select.Option>
            <Select.Option value={2}>Медсестра</Select.Option>
            <Select.Option value={3}>Хирург</Select.Option>
          </Select>
        </Form.Item>
        <Form.Item name="Description" label="Описание">
          <Input.TextArea rows={4} />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            {employeeDepartment ? 'Сохранить изменения' : 'Добавить связь'}
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default DepartmentEmployeeFormModal;
