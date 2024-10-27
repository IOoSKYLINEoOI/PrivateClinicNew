'use client';
import React, { useEffect } from 'react';
import { Modal, Form, Input, Button, DatePicker, message } from 'antd';
import { EmployeeRequest, EmployeeResponse } from '@/models/Employee';
import { createEmployee, updateEmployee } from '@/utils/api';
import moment from 'moment';

interface EmployeeFormModalProps {
  visible: boolean;
  onClose: () => void;
  employee?: EmployeeResponse | null;
}

const EmployeeFormModal: React.FC<EmployeeFormModalProps> = ({ visible, onClose, employee }) => {
  const [form] = Form.useForm();

  useEffect(() => {
    if (employee) {
      form.setFieldsValue({
        HiringDate: moment(employee.HiringDate),
        DateOfDismissal: employee.DateOfDismissal ? moment(employee.DateOfDismissal) : null,
        Description: employee.Description,
        UserId: employee.UserId,
      });
    } else {
      form.resetFields();
    }
  }, [employee, form]);

  const onFinish = async (values: any) => {
    try {
      const formattedValues: EmployeeRequest = {
        ...values,
        HiringDate: values.HiringDate.format('YYYY-MM-DD'),
        DateOfDismissal: values.DateOfDismissal ? values.DateOfDismissal.format('YYYY-MM-DD') : null,
      };

      if (employee) {
        await updateEmployee(employee.id, formattedValues);
        message.success("Сотрудник обновлен");
      } else {
        await createEmployee(formattedValues);
        message.success("Сотрудник добавлен");
      }
      onClose();
    } catch (error) {
      message.error("Ошибка при сохранении сотрудника");
    }
  };

  return (
    <Modal
      title={employee ? "Редактировать сотрудника" : "Добавить сотрудника"}
      visible={visible}
      onCancel={onClose}
      footer={null}
    >
      <Form form={form} layout="vertical" onFinish={onFinish}>
        <Form.Item name="HiringDate" label="Дата приема" rules={[{ required: true, message: "Укажите дату приема" }]}>
          <DatePicker format="YYYY-MM-DD" />
        </Form.Item>
        <Form.Item name="DateOfDismissal" label="Дата увольнения">
          <DatePicker format="YYYY-MM-DD" />
        </Form.Item>
        <Form.Item name="Description" label="Описание">
          <Input.TextArea rows={4} />
        </Form.Item>
        <Form.Item
          name="UserId"
          label="Идентификатор пользователя"
          rules={[{ required: true, message: "Введите идентификатор пользователя" }]}
        >
          <Input placeholder="Введите ID пользователя вручную" />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            {employee ? "Сохранить изменения" : "Добавить сотрудника"}
          </Button>
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default EmployeeFormModal;
