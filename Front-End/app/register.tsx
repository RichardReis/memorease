import React from "react";
import { Dimensions, StyleSheet, Text, View } from "react-native";
import BuildForm from "../components/Form/BuildForm";
import { BuildInputsType } from "../components/Form/BuildInputs";
import { Link, Stack, useRouter } from "expo-router";
import Colors from "../constants/Colors";
import TextStyles from "../themedStyles/Text";
import Spacing from "../constants/Spacing";
import * as Account from "../service/Account/register";
import { AuthUser } from "../service/Account/login";

const Register: React.FC = () => {
  const router = useRouter();
  const inputs: BuildInputsType = [
    {
      name: "name",
      label: "Nome",
      required: true,
      type: "text",
    },
    {
      name: "email",
      label: "E-mail",
      required: true,
      type: "text",
    },
    {
      name: "password",
      label: "Senha",
      required: true,
      type: "password",
    },
  ];

  const register = async (data: any) => {
    let response = await Account.Register(data);
    if (response) {
      let result = await AuthUser(data);
      if (result) router.push("/(tabs)");
    }
  };

  return (
    <View style={styles.container}>
      <View style={styles.titleArea}>
        <Text style={styles.title}>Cadastre-se</Text>
        <Text style={styles.subtitle}>
          Retenha o conhecimento de forma simples e de um boot na sua vida
          academica!
        </Text>
      </View>
      <View style={styles.content}>
        <BuildForm buttonText="Cadastrar" inputs={inputs} onSubmit={register} />
        <View style={styles.row}>
          <Text style={styles.text}>Já possui uma conta? </Text>
          <Link href={"/login"}>
            <Text style={styles.link}>Entrar</Text>
          </Link>
        </View>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: "center",
    backgroundColor: Colors["light"].primary,
  },
  titleArea: {
    height: Dimensions.get("screen").height * 0.3,
    justifyContent: "flex-end",
    paddingHorizontal: Spacing.xg,
    paddingVertical: Spacing.s,
  },
  title: { ...TextStyles.titleHeader, fontSize: 32 },
  subtitle: { ...TextStyles.subTitleHeader, fontSize: 16 },
  content: {
    flex: 1,
    padding: Spacing.xg,
    backgroundColor: Colors["light"].contentBackground,
    borderTopRightRadius: Spacing.g,
    borderTopLeftRadius: Spacing.g,
  },
  text: { ...TextStyles.regular },
  link: {
    ...TextStyles.regular,
    ...TextStyles.link,
  },
  row: {
    marginTop: Spacing.g,
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
  },
});

export default Register;
