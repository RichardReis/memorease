import React from "react";
import { useForm } from "../../../hooks/useForm";
import Button from "../../Button";

interface SubmitButtonProps {
  title: string;
}

const SubmitButton: React.FC<SubmitButtonProps> = ({ title }) => {
  const { submit } = useForm();

  return <Button title={title} onPress={submit} type="primary" />;
};

export default SubmitButton;
