import React from "react";
import { BuildInputsType } from "../components/Form/BuildInputs";
import FormScreenStructure from "../components/FormScreenStructure";
import { ChangeData } from "../service/Account/changeData";

const EditProfile: React.FC = () => {
  const inputs: BuildInputsType = [
    {
      name: "name",
      label: "Nome",
      required: false,
      type: "text",
    },
    {
      name: "email",
      label: "E-mail",
      required: false,
      type: "text",
    },
  ];

  const edit = async (data: any) => {
    let response = await ChangeData(data);
  };

  return (
    <>
      <FormScreenStructure
        inputs={inputs}
        title="Alterar Dados"
        buttonText="Salvar"
        onSubmit={edit}
      />
    </>
  );
};

export default EditProfile;
