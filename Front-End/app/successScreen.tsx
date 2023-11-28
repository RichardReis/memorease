import React, { useEffect } from "react";
import Constants from "expo-constants";
import { StyleSheet, View, Text } from "react-native";
import TextStyles from "../themedStyles/Text";
import LottieView from "lottie-react-native";
import {
  Stack,
  useLocalSearchParams,
  useNavigation,
  useRouter,
} from "expo-router";
import Spacing from "../constants/Spacing";
import Button from "../components/Button";

const SuccessScreen: React.FC = () => {
  const navigation = useNavigation();
  const router = useRouter();
  const params = useLocalSearchParams();
  const { text, redirect } = params;

  useEffect(() => {
    navigation.addListener("beforeRemove", (e) => {
      e.preventDefault();
      if (e.data.action.type !== "GO_BACK") {
        navigation.dispatch(e.data.action);
      }
    });
  }, []);

  const RedirectTo = () => router.push(redirect as string);

  return (
    <View style={styles.container}>
      <Stack.Screen options={{ headerShown: false, headerLeft: () => null }} />
      <LottieView
        style={{ width: "80%", aspectRatio: 1 }}
        source={require("../assets/lottie/check.json")}
        autoPlay
      />
      <Text style={{ ...TextStyles.title, textAlign: "center" }}>{text}</Text>
      <Button type="primary" title="Ok" onPress={() => RedirectTo()} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: Constants.statusBarHeight,
    paddingHorizontal: Spacing.g,
    alignItems: "center",
    justifyContent: "center",
    gap: Spacing.m,
  },
});

export default SuccessScreen;
