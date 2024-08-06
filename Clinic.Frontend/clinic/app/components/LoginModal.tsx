"use client";

import { Modal, Input, DatePicker, Form, Button } from "antd";
import { UserOutlined, MailOutlined, LockOutlined, PhoneOutlined } from "@ant-design/icons";
import React from "react";
import { LoginUserRequest } from "../services/user";

interface LoginModalProps {
  visible: boolean;
  onLogin: (values: LoginUserRequest) => void;
  onCancel: () => void;
}

const RegisterModal: React.FC<LoginModalProps> = ({ visible, onLogin, onCancel }) => {
  const [form] = Form.useForm();

  const handleOk = () => {
    form.validateFields()
      .then(async values => {
        const formattedValues: LoginUserRequest = {
          email: values.email,
          password: values.password, 
        };

        try {

          await onLogin(formattedValues);
          form.resetFields(); 
        } catch (error) {
          console.error("Ошибка при аутенфикации пользователя:", error);
        }
      })
      .catch(info => {
        console.log("Ошибка валидации:", info);
      });
  };

  return (
    <Modal
      title="Вход в профиль"
      visible={visible}
      onOk={handleOk}
      onCancel={onCancel}
      footer={[
        <Button key="back" onClick={onCancel}>
          Отмена
        </Button>,
        <Button key="submit" type="primary" onClick={handleOk}>
          Войти
        </Button>,
      ]}
    >
      <Form form={form} layout="vertical" name="login_form">

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
