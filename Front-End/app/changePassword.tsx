import React from "react";
import { BuildInputsType } from "../components/Form/BuildInputs";
import FormScreenStructure from "../components/FormScreenStructure";

const ChangePassword: React.FC = () => {
  const inputs: BuildInputsType = [
    {
      name: "oldpassword",
      placeholder: "Senha Atual",
      required: true,
      type: "password",
    },
    {
      name: "newpassword",
      placeholder: "Nova Senha",
      required: true,
      type: "password",
    },
    {
      name: "newpasswordconfirm",
      placeholder: "Confirmar Nova Senha",
      required: true,
      type: "password",
    },
  ];

  return (
    <FormScreenStructure
      inputs={inputs}
      title="Alterar Senha"
      buttonText="Salvar"
    />
  );
};

export default ChangePassword;
