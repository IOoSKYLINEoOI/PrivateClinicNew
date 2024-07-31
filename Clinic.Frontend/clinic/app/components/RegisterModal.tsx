"use client";

import { Modal, Input, DatePicker, Form, Button } from "antd";
import { UserOutlined, MailOutlined, LockOutlined, PhoneOutlined } from "@ant-design/icons";
import moment from "moment";
import React from "react";
import { RegisterUserRequest } from "../services/user";

interface RegisterModalProps {
  visible: boolean;
  onRegister: (values: RegisterUserRequest) => void;
  onCancel: () => void;
}

const RegisterModal: React.FC<RegisterModalProps> = ({ visible, onRegister, onCancel }) => {
  const [form] = Form.useForm();

  const handleOk = () => {
    form.validateFields()
      .then(async values => {
        const formattedValues: RegisterUserRequest = {
          firstName: values.firstName,
          lastName: values.lastName,
          fatherName: values.fatherName || "", 
          dateOfBirth: values.dateOfBirth.format("YYYY-MM-DD"), 
          email: values.email,
          phoneNumber: values.phoneNumber,
          password: values.password, 
        };

        try {

          await onRegister(formattedValues);
          form.resetFields(); 
        } catch (error) {
          console.error("Ошибка при регистрации пользователя:", error);
        }
      })
      .catch(info => {
        console.log("Ошибка валидации:", info);
      });
  };

  return (
    <Modal
      title="Регистрация"
      visible={visible}
      onOk={handleOk}
      onCancel={onCancel}
      footer={[
        <Button key="back" onClick={onCancel}>
          Отмена
        </Button>,
        <Button key="submit" type="primary" onClick={handleOk}>
          Зарегистрироваться
        </Button>,
      ]}
    >
      <Form form={form} layout="vertical" name="register_form">
        <Form.Item
          name="firstName"
          label="Имя"
          rules={[{ required: true, message: "Пожалуйста, введите ваше имя!" }]}
        >
          <Input prefix={<UserOutlined />} placeholder="Имя" />
        </Form.Item>

        <Form.Item
          name="lastName"
          label="Фамилия"
          rules={[{ required: true, message: "Пожалуйста, введите вашу фамилию!" }]}
        >
          <Input prefix={<UserOutlined />} placeholder="Фамилия" />
        </Form.Item>

        <Form.Item name="fatherName" label="Отчество">
          <Input prefix={<UserOutlined />} placeholder="Отчество (необязательно)" />
        </Form.Item>

        <Form.Item
          name="dateOfBirth"
          label="Дата рождения"
          rules={[{ required: true, message: "Пожалуйста, выберите вашу дату рождения!" }]}
        >
          <DatePicker style={{ width: "100%" }} placeholder="Дата рождения" />
        </Form.Item>

        <Form.Item
          name="email"
          label="Электронная почта"
          rules={[
            { required: true, message: "Пожалуйста, введите вашу электронную почту!" },
            { type: "email", message: "Неверный формат электронной почты!" }
          ]}
        >
          <Input prefix={<MailOutlined />} placeholder="Электронная почта" />
        </Form.Item>

        <Form.Item
          name="phoneNumber"
          label="Номер телефона"
          rules={[{ required: true, message: "Пожалуйста, введите ваш номер телефона!" }]}
        >
          <Input prefix={<PhoneOutlined />} placeholder="Номер телефона" />
        </Form.Item>

        <Form.Item
          name="password"
          label="Пароль"
          rules={[{ required: true, message: "Пожалуйста, введите ваш пароль!" }]}
        >
          <Input.Password prefix={<LockOutlined />} placeholder="Пароль" />
        </Form.Item>
      </Form>
    </Modal>
  );
};

export default RegisterModal;
